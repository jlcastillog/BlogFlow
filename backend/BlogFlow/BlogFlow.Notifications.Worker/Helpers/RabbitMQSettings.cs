﻿namespace BlogFlow.Notifications.Worker.Helpers
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
