using System.Collections.Generic;
using System.IO;
using log4net.Core;
using Newtonsoft.Json;

namespace log4net.Layout
{
    public class JsonLayout : LayoutSkeleton
    {

        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent e)
        {
            var dic = new Dictionary<string, object>
            {
                ["level"] = e.Level.DisplayName,
                ["renderedMessage"] = e.RenderedMessage,
                ["timestampUtc"] = e.TimeStamp.ToUniversalTime().ToString("O"),
                ["logger"] = e.LoggerName,
                ["thread"] = e.ThreadName,
                ["exceptionObject"] = e.ExceptionObject,
                ["exceptionObjectString"] = e.ExceptionObject == null ? null : e.GetExceptionString(),
            };

            if (e.GetProperties() != null)
            {
                foreach (var key in e.GetProperties().GetKeys())
                {
                    var value = e.GetProperties()[key];
                    if (value != null)
                        dic.Add(key, value.ToString());
                }
            }

            writer.Write(JsonConvert.SerializeObject(dic));
        }
    }
}