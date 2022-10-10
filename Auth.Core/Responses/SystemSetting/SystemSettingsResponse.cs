


using System.Collections.Generic;
using Auth.Core.Contract;
using Auth.Core.Dao;

namespace Auth.Core.Response
{

    public class SystemSettingsResponse : ResponseBaseObject
    {
        public IList<SystemSetting> Settings { get; set; }
    }

    public class SystemSettingResponse : ResponseBaseObject
    {
        public SystemSetting Setting { get; set; }
    }

    public class AddSystemSettingResponse : ResponseBaseObject
    {
    }
    
}