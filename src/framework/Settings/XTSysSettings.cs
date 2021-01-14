using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Orion.Framework.Validations;

namespace Orion.Framework.Settings
{
    public class XTSysSettingsFolders
    {
        public string Upload { get; set; }
        public string WebRoot { get; set; }
        public string Images { get; set; }
        public string Icons { get; set; }
        public string Fonts { get; set; }
        public string Data { get; set; }
        public string Mappings { get; set; }
        public string Sql { get; set; }
    }
    public class XTSysSettingsMenu
    {
        public string ConnectionString { get; set; }
        public string ConnectionKey { get; set; }
       
    }
    public class XTSysSettingsDatabase
    {
        public XTNHSettings NHOptions { get; set; }
        public string ConnectionString { get; set; }
        public bool UseDatabaseSchema { get; set; } = true;
    }
    public class XTSysSettingsApplication
    {
        public string Id { get; set; }

        public string Name { get; set; }
       
        public string UserName { get; set; }
        public string UserId { get; set; }
      
        public int? OrganizationCode { get; set; } = 0;
        public string OrganizationName { get; set; }
        public bool ControlAccessEnabled { get; set; }
    }

    public interface IXTSysSettings
    {
        XTSysSettingsFolders Folder { get; set; }
        XTSysSettingsDatabase Database { get; set; }
        XTSysSettingsApplication Settings { get; set; }
        XTSysSettingsMenu Menu { get; set; }
        IXTSysSettings AddSysFolders(string webrootfolder);
        IXTSysSettings AddDatabase();
        IXTSysSettings AddMenu();
        IXTSysSettings AddSysSettings();
        public IXTSysSettings Initialize(IServiceCollection services);

    }

    public class XTSysSettings : IXTSysSettings
    {
        private  IServiceCollection _services;
        private  IConfiguration _configuration;
        XTNHSettings _nhsettings;
        public XTSysSettings()
        {
            this.Folder = new XTSysSettingsFolders();
            this.Database = new XTSysSettingsDatabase();
            this.Settings = new XTSysSettingsApplication();
            this.Menu = new XTSysSettingsMenu();
        }
        public XTSysSettingsMenu Menu { get; set; }
        public  XTSysSettingsFolders Folder { get; set; }
        public XTSysSettingsDatabase Database { get; set; }
        public XTSysSettingsApplication Settings { get; set; }

        public IXTSysSettings Initialize(IServiceCollection services)
        {
            Ensure.NotNull(services, "collection services invalid");
            this._services = services;
            this._configuration = _services.BuildServiceProvider().GetService<IConfiguration>();
            this._nhsettings = _configuration.GetSection(XTNHSettings.SECTION_NAME).Get<XTNHSettings>();

            Ensure.NotNull(_configuration, "configuration service invalid!");
            Ensure.NotNull(_nhsettings, "nhibernate configuration service invalid!");
            return this;
        }
        void IsValid()
        {

            Ensure.NotNull(_services, "collection services invalid");
            Ensure.NotNull(_configuration, "configuration service invalid!");
            Ensure.NotNull(_nhsettings, "nhibernate configuration service invalid!");

        }
        public IXTSysSettings AddSysSettings()
        {
            IsValid();

            var appName = _configuration["AppSettings:ApplicationName"];
            var controlAccess = _configuration["AppSettings:ControlAccessEnabled"];
            this.Settings.Name = appName;
            this.Settings.UserId = "1";
            this.Settings.OrganizationCode = 0;
            this.Settings.ControlAccessEnabled = controlAccess.To<bool>();
            return this;

        }
        public IXTSysSettings AddMenu()
        {
            var _configuration = _services.BuildServiceProvider().GetService<IConfiguration>();

            var menuConnection = _configuration["MenuSettings:ConnectionStringName"];
            var keyConnection = _configuration["MenuSettings:ConnectionKeyName"];
            var sqlConnex = _configuration.GetConnectionString(menuConnection);


            this.Menu.ConnectionString= sqlConnex;
            this.Menu.ConnectionKey = keyConnection;
            return this;
        }
        public IXTSysSettings AddDatabase()
        {
            var _configuration = _services.BuildServiceProvider().GetService<IConfiguration>();
            XTNHSettings options = _configuration.GetSection(XTNHSettings.SECTION_NAME).Get<XTNHSettings>();
            var sqlConnex = _configuration.GetConnectionString(options.ConnectionStringName);

            this.Database.NHOptions = options;
            this.Database.UseDatabaseSchema = options.UseSchema;
            this.Database.ConnectionString = sqlConnex;
            return this;
        }
        public IXTSysSettings AddSysFolders(string webroot)
        {
            IsValid();
            
            var iconsFolder = "icons";
            var sqlFolder = "sql";
            var dataFolder = _configuration["AppSettings:WebDataFolder"];
            var uploadFolder = _configuration["AppSettings:UploadFolder"];
            var imagesFolder = _configuration["AppSettings:ImagesFolder"];
            var fontsFolder = _configuration["AppSettings:FontsFolder"];
            var folderMappings = _nhsettings.FolderMappings;

            if (dataFolder.IsEmpty()) dataFolder = "data";
            if (folderMappings.IsEmpty()) folderMappings = "mappings";
            if (uploadFolder.IsEmpty()) uploadFolder = "upload";
            if (imagesFolder.IsEmpty()) imagesFolder = "images";
            if (fontsFolder.IsEmpty()) fontsFolder = "fonts";

            dataFolder = Path.Combine(webroot, dataFolder);
            if (!System.IO.Directory.Exists(dataFolder))
            {
                System.IO.Directory.CreateDirectory(dataFolder);
            }
            uploadFolder = Path.Combine(dataFolder, uploadFolder);
            if (!System.IO.Directory.Exists(uploadFolder))
            {
                System.IO.Directory.CreateDirectory(uploadFolder);
            }
            folderMappings = Path.Combine(dataFolder, folderMappings);
            if (!System.IO.Directory.Exists(folderMappings))
            {
                System.IO.Directory.CreateDirectory(folderMappings);
            }
            imagesFolder = Path.Combine(dataFolder, imagesFolder);
            if (!System.IO.Directory.Exists(imagesFolder))
            {
                System.IO.Directory.CreateDirectory(imagesFolder);
            }            
            iconsFolder = Path.Combine(imagesFolder, iconsFolder);
            if (!System.IO.Directory.Exists(iconsFolder))
            {
                System.IO.Directory.CreateDirectory(iconsFolder);
            }
            fontsFolder = Path.Combine(dataFolder, fontsFolder);
            if (!System.IO.Directory.Exists(fontsFolder))
            {
                System.IO.Directory.CreateDirectory(fontsFolder);
            }
            sqlFolder = Path.Combine(dataFolder, sqlFolder);
            if (!System.IO.Directory.Exists(sqlFolder))
            {
                System.IO.Directory.CreateDirectory(sqlFolder);
            }
            this.Folder.WebRoot = webroot;
            this.Folder.Data = dataFolder;
            this.Folder.Upload = uploadFolder;
            this.Folder.Images = imagesFolder;
            this.Folder.Fonts = fontsFolder;
            this.Folder.Icons = iconsFolder;
            this.Folder.Mappings = folderMappings;
            this.Folder.Sql = sqlFolder;

            return this;
        }


    }

}
