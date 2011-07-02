using System;
using PR.Chat.Core.BusinessObjects;

namespace PR.Chat.DAL.EF
{
    public partial class Nick : INick
    {


        private NickSettings _nickSettings;
        public NickSettings Settings
        {
            get
            {
                if (_nickSettings == null)
                {
                    _nickSettings = Serializator.Deserialze<NickSettings>(SettingsBinary);
                    _nickSettings.PropertyChanged += (sender, args) => SettingsBinary = Serializator.Serialize(Settings);    
                }
                return _nickSettings;
            }
        }
    }
}