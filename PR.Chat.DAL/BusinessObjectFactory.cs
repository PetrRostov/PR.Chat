using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PR.Chat.Core.BusinessObjects;
using PR.Chat.DAL.EF;

namespace PR.Chat.DAL
{
    public class BusinessObjectFactory : IBusinessObjectFactory
    {
        public IAccount CreateAccount(string email)
        {
            return new Account();
        }

        public IChannel CreateChannel(string name, bool isHidden, bool isTemporary)
        {
            return new Channel {
                Name = name, 
                IsHidden = isHidden, 
                IsTemporary = isTemporary
            };
        }

        public IChannelMessage CreateChannelMessage(INick from, string text, IChannel channel)
        {
            throw new NotImplementedException();
        }

        public IPrivateMessage CreatePrivateMessage(INick from, string text, INick to)
        {

            throw new NotImplementedException();
        }

        public INick CreateNick(string name)
        {
            return new Nick {
                Name            = name,
                SettingsBinary  = Serializator.Serialize(NickSettings.Default)
            };
        }
    }
}
