namespace OrleansTestAPI
{
    public class GrainStorageConfig
    {
        /// <summary>
        /// orleansStorage名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 存储提供程序名称
        /// </summary>
        public string Invariant { get; set; } = string.Empty;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 是否使用Json格式存储数据
        /// </summary>
        public bool UseJsonFormat { get; set; }
        /// <summary>
        /// 集群Id
        /// </summary>
        public string ClusterId { get; set; } = string.Empty;
        /// <summary>
        /// 服务Id
        /// </summary>
        public string ServiceId { get; set; } = string.Empty;
        /// <summary>
        /// 是否启用监控
        /// </summary>
        public bool IsActivateMonitoring { get; set; }
        /// <summary>
        /// 监控用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// 监控密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 主机 默认:*
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// 计数器更新间隔 
        /// </summary>

        public int CounterUpdateIntervalMs { get; set; }
        /// <summary>
        /// 是否启用主机自身
        /// </summary>
        public bool HostSelf { get; set; }
        /// <summary>
        /// silo ip
        /// </summary>
        public string IpString { get; set; } = string.Empty;
        /// <summary>
        /// silo port
        /// </summary>
        public int SiloPort { get; set; }
        /// <summary>
        /// gateway port
        /// </summary>
        public int GatewayPort { get; set; }
    }
}
