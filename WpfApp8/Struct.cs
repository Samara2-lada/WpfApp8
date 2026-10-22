namespace RaceWPF
{
    /// <summary>
    /// Структура, описывающая участника забега на 100 метров.
    /// </summary>
    public struct Participant
    {
        public string FullName { get; set; }   // ФИО
        public int Number { get; set; }        // Номер участника
        public double Result { get; set; }     // Результат, сек
        public Participant(string fullName, int number, double result)
        {
            FullName = fullName;
            Number = number;
            Result = result;
        }
    }
}