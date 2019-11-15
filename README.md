# Log4net.JsonLayout

From https://github.com/Litee/log4net.Layout.Json

## How to use it

```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
    <appender name="Console" type="log4net.Appender.ConsoleAppender">
        <layout type="log4net.Layout.JsonLayout,Log4net.JsonLayout">
        </layout>
    </appender>
    <root>
        <level value="DEBUG" />
        <appender-ref ref="Console" />
    </root>
</log4net>

```
