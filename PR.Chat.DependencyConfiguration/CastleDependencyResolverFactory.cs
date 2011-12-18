using System.Linq;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using PR.Chat.Application;
using PR.Chat.Domain;
using PR.Chat.Infrastructure;
using PR.Chat.Infrastructure.Castle;
using PR.Chat.Infrastructure.Data;
using PR.Chat.Infrastructure.Data.NH;
using PR.Chat.Infrastructure.UnitOfWork;
using PR.Chat.Presentation.Web.Core;
using IDependencyResolver = PR.Chat.Infrastructure.IDependencyResolver;
using MembershipService = PR.Chat.Application.MembershipService;

namespace PR.Chat.DependencyConfiguration
{
    
}