using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNFactoryAutoSystem
{
    public static class appString
    {
        /// <summary>
        /// 获取配置文件里appsettings的数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetAppsettingStr(string str)
        {
            AppSettingsReader appReader = new AppSettingsReader();
            return appReader.GetValue(str, typeof(string)).ToString();
        }

        private static string getString(string section, string key)
        {
            try
            {
                Configuration configuration = config;
                AppSettingsSection newSection = appSection(section);
                if (newSection.Settings[key] == null) { return ""; }
                else { return newSection.Settings[key].Value; }
            }
            catch { return ""; }
        }

        private static void setString(string section, string key, string value)
        {
            try
            {
                Configuration configuration = config;
                AppSettingsSection appSetting = appSection(section);
                if (appSetting.Settings[key] == null) { appSetting.Settings.Add(key, value); }
                else { appSetting.Settings[key].Value = value; }
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(section);
            }
            catch (System.Exception Ex) { }
        }

        public static string conncetString { set; get; }
        private static string _configFile = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\appString\user.config";
        private static string configFile { get { return _configFile; } }
        private static Configuration _config;

        public static Configuration config
        {
            get
            {
                if (_config != null) { return _config; }
                else
                {
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = configFile;
                    _config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    return _config;
                }
            }
        }

        public static AppSettingsSection appSection(string sectionName)
        {
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection(sectionName);
            if (appSetting != null) { return appSetting; }
            try
            {
                appSetting = new AppSettingsSection();
                config.Sections.Add(sectionName, appSetting);
                appSetting.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                return appSetting;
            }
            catch (ConfigurationErrorsException Ex) { return null; }
        }
    }
}
