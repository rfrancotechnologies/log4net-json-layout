using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using log4net.Core;
using Newtonsoft.Json;

namespace log4net.Layout
{
    public class JsonLayout : LayoutSkeleton
    {

        static readonly string ProcessSessionId = Guid.NewGuid().ToString();
        static readonly int ProcessId = Process.GetCurrentProcess().Id;
        static readonly string MachineName = Environment.MachineName;

        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent e)
        {
            var dic = new Dictionary<string, object>
            {
                ["processSessionId"] = ProcessSessionId,
                ["level"] = e.Level.DisplayName,
                ["renderedMessage"] = e.RenderedMessage,
                ["timestampUtc"] = e.TimeStamp.ToUniversalTime().ToString("O"),
                ["logger"] = e.LoggerName,
                ["thread"] = e.ThreadName,
                ["exceptionObject"] = e.ExceptionObject,
                ["exceptionObjectString"] = e.ExceptionObject == null ? null : e.GetExceptionString(),
                ["userName"] = e.UserName,
                ["domain"] = e.Domain,
                ["identity"] = e.Identity,
                ["location"] = e.LocationInformation.FullInfo,
                ["pid"] = ProcessId,
                ["machineName"] = MachineName,
                ["workingSet"] = Environment.WorkingSet,
                ["osVersion"] = Environment.OSVersion.ToString(),
                ["is64bitOS"] = Environment.Is64BitOperatingSystem,
                ["is64bitProcess"] = Environment.Is64BitProcess,
            };
            
            if (e.GetProperties() != null)
                foreach (var key in e.GetProperties().GetKeys())
                {
                    var value = e.GetProperties()[key];
                    if (value != null)
                        dic.Add(key, value.ToString());
                }

            writer.Write(JsonConvert.SerializeObject(dic));
        }
    }
}