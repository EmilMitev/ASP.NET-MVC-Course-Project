﻿namespace StackFaceSystem.Services.Contracts.Data
{
    public interface IVotesService
    {
        int RegisterVote(string userId, string subject, int id, int voteType);
    }
}