using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Messages;
using DotNetOpenAuth.OpenId.RelyingParty;
using LittleProblem.Data.Model;
using LittleProblem.Data.Services;
using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Web.Extension.OpenId
{
    public class FakeAccountRelyingParty : IAccountRelyingParty
    {
        private readonly IMembershipService _membershipService;
        private readonly ISessionRegistry _sessionRegistry;

        public FakeAccountRelyingParty(IMembershipService membershipService, ISessionRegistry sessionRegistry)
        {
            _membershipService = membershipService;
            _sessionRegistry = sessionRegistry;
        }

        public IAuthenticationResponse GetResponse()
        {
            return new FakeSuccesResponse();
        }

        public bool IsValidIdentifier(string identifier)
        {
            return true;
        }

        public ActionResult CreateRequest(string identifier)
        {
            throw new NotImplementedException();
        }

        public void LogInMember(IAuthenticationResponse response)
        {
            Member connectedMember = _membershipService.LogIn("http://quentinproust@openid.com", "q.proust@gmail.com");

            _sessionRegistry.MemberInformations.UserName = connectedMember.UserName;
            _sessionRegistry.MemberInformations.OpenId = "http://quentinproust@openid.com";
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }

    internal class FakeSuccesResponse : IAuthenticationResponse
    {
        public string GetCallbackArgument(string key)
        {
            throw new NotImplementedException();
        }

        public string GetUntrustedCallbackArgument(string key)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetCallbackArguments()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetUntrustedCallbackArguments()
        {
            throw new NotImplementedException();
        }

        public T GetExtension<T>() where T : IOpenIdMessageExtension
        {
            throw new NotImplementedException();
        }

        public IOpenIdMessageExtension GetExtension(Type extensionType)
        {
            throw new NotImplementedException();
        }

        public T GetUntrustedExtension<T>() where T : IOpenIdMessageExtension
        {
            throw new NotImplementedException();
        }

        public IOpenIdMessageExtension GetUntrustedExtension(Type extensionType)
        {
            throw new NotImplementedException();
        }

        public Identifier ClaimedIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public string FriendlyIdentifierForDisplay
        {
            get { throw new NotImplementedException(); }
        }

        public AuthenticationStatus Status
        {
            get { return AuthenticationStatus.Authenticated; }
        }

        public IProviderEndpoint Provider
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Exception
        {
            get { throw new NotImplementedException(); }
        }
    }
}