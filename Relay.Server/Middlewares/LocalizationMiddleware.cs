using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Relay.Middlewares
{
    // Middleware для обработки и установки языка в зависимости от запроса.
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next; // Ссылка на следующий middleware в конвейере обработки запросов.
        private readonly ILogger<LocalizationMiddleware> _logger; // Логгер для записи информации о процессе.

        // Конструктор принимает следующий middleware и логгер.
        public LocalizationMiddleware(RequestDelegate next, ILogger<LocalizationMiddleware> logger)
        {
            _next = next; // Инициализация следующего middleware.
            _logger = logger; // Инициализация логгера.
        }

        // Основной метод, обрабатывающий запросы и задающий язык.
        public async Task InvokeAsync(HttpContext context)
        {
            // Определяем язык на основе строки запроса или заголовков.
            var language = DetermineRequestLanguage(context);

            // Устанавливаем выбранный язык в контексте запроса для дальнейшего использования.
            context.Items["Language"] = language;

            // Логируем информацию о выбранном языке.
            _logger.LogInformation("Установлен язык: {Language}", language.Name);

            // Передача управления следующему middleware в конвейере.
            await _next(context);
        }

        // Метод для определения языка на основе строки запроса или заголовка Accept-Language.
        private CultureInfo DetermineRequestLanguage(HttpContext context)
        {
            // Проверка параметра "lang" в строке запроса.
            var langQuery = context.Request.Query["lang"].ToString();
            if (!string.IsNullOrEmpty(langQuery) && IsLanguageValid(langQuery))
            {
                _logger.LogInformation("Язык из параметра запроса: {LangQuery}", langQuery);
                return new CultureInfo(langQuery); // Возвращаем объект CultureInfo для указанного языка.
            }

            // Проверка заголовка Accept-Language для определения предпочтительного языка пользователя.
            var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();
            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                var languages = acceptLanguage.Split(',');
                if (languages.Length > 0 && IsLanguageValid(languages[0]))
                {
                    _logger.LogInformation("Язык из заголовка: {HeaderLanguage}", languages[0]);
                    return new CultureInfo(languages[0]); // Возвращаем объект CultureInfo для первого допустимого языка из заголовка.
                }
            }

            // Возвращаем язык по умолчанию при ошибке или отсутствии данных.
            _logger.LogWarning("Язык не указан или недействителен. Установлен язык по умолчанию: 'ru'");
            return new CultureInfo("ru"); // Устанавливаем русский язык по умолчанию.
        }

        // Метод для проверки валидности языка.
        private bool IsLanguageValid(string languageName)
        {
            try
            {
                CultureInfo.GetCultureInfo(languageName); // Проверяем, существует ли культура с данным именем.
                return true; // Язык валиден, возвращаем true.
            }
            catch (CultureNotFoundException)
            {
                _logger.LogWarning("Некорректный язык: {InvalidLanguage}", languageName); // Логируем предупреждение о некорректном языке.
                return false; // Язык не валиден, возвращаем false.
            }
        }
    }
}
