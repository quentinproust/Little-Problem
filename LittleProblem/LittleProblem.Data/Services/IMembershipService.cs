﻿using LittleProblem.Data.Model;

namespace LittleProblem.Data.Services
{
    /// <summary>
    /// Deals with member control in database.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Log a user in.
        /// </summary>
        /// <param name="openId">OpenId Identifier</param>
        Member LogIn(string openId);

        /// <summary>
        /// Log a user in.
        /// </summary>
        /// <param name="openId">OpenId Identifier</param>
        /// <param name="email">Email</param>
        Member LogIn(string openId, string email);

        /// <summary>
        /// Edit member profile.
        /// </summary>
        /// <param name="member">Member with edited fields</param>
        void EditMemberProfile(Member member);

        /// <summary>
        /// Will update note for all users.
        /// </summary>
        void UpdateUserNote();
    }
}
