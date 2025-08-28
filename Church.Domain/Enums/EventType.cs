using System.ComponentModel;

namespace Church.Domain.Enums
{
    public enum EventType
    {
        [Description("Culto")]
        Service = 1,

        [Description("Encontro")]
        Meeting = 2,

        [Description("Reunião de Oração")]
        Prayer = 3,

        [Description("Estudo Bíblico")]
        Fellowship = 4,

        [Description("Ação Social")]
        Outreach = 5,

        [Description("Outro")]
        Other = 6
    }
}
