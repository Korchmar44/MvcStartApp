using AuthenticationService.Interfaces;

namespace AuthenticationService
{
    public class Logger : ILoggerApp
    {
        private readonly string logsDirectory;

        public Logger()
        {
            // Получаем путь к текущей директории, где запущен исполняемый файл
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Создание уникальной директории на основе текущей даты и времени в каталоге bin\Debug
            logsDirectory = Path.Combine(baseDirectory, "Logs", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            // Создание директории
            Directory.CreateDirectory(logsDirectory);
        }

        public void WriteEvent(string eventMessage)
        {
            // Записываем сообщение о событии в файл events.txt
            WriteToFile(Path.Combine(logsDirectory, "events.txt"), eventMessage);
        }

        public void WriteError(string errorMessage)
        {
            // Записываем сообщение об ошибке в файл errors.txt
            WriteToFile(Path.Combine(logsDirectory, "errors.txt"), errorMessage);
        }

        private void WriteToFile(string filePath, string message)
        {
            try
            {
                // Используем using для автоматического закрытия потока
                using (StreamWriter writer = new StreamWriter(filePath, true)) // true для добавления в конец файла
                {
                    writer.WriteLine($"{DateTime.Now}: {message}"); // добавляем текущую дату и время
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
    }
}
