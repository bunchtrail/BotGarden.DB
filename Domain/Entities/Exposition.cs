namespace Domain.Entities
{
    public class Exposition
    {
        public int ExpositionId { get; set; }
        public string ExpositionName { get; set; } // "Дендрология", "Флора", "Цветоводство" и т.д.
        public string Description { get; set; } // Опционально
    }
}
