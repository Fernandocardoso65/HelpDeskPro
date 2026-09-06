using System;

namespace HelpDeskWeb.Helpers
{
    public static class HorarioBrasil
    {
        private static readonly TimeZoneInfo FusoHorario =
            TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

        public static DateTime Agora()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                FusoHorario
            );
        }
    }
}
