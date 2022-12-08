using System;
using System.Diagnostics;
using System.Reflection;

namespace DeveReproduceXmlSerializerBug.Status
{
    public static class StatusObtainer
    {
        public static StatusModel GetStatus()
        {
            var statusModel = new StatusModel(
                Assembly.GetEntryAssembly()?.GetName()?.Name ?? "<None>",
                Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString() ?? "<NoVersion>",
                (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString());

            return statusModel;
        }
    }
}
