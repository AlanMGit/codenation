using SoccerTeamsManager.Exceptions;
using SoccerTeamsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SoccerTeamsManager
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<Team> _teams;
        private List<Player> _players;

        public SoccerTeamsManager() 
        {
            _teams = new List<Team>();
            _players = new List<Player>();
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (HasPlayer(id))
                throw new UniqueIdentifierException();

            if (!HasTeam(teamId))
                throw new TeamNotFoundException();

            _players.Add(new Player
            {
                Id = id,
                TeamId = teamId,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            });
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {

            if (HasTeam(id))
                throw new UniqueIdentifierException();

            _teams.Add(new Team
            {
                Id = id,
                Name = name,
                Created = createDate,
                MainShirtColor = mainShirtColor,
                SecondaryShirtColor = secondaryShirtColor
            });
        }

        public long GetBestTeamPlayer(long teamId)
        {
            ValidateTeam(teamId);

            return _players.Where(x => x.TeamId == teamId)
                           .OrderByDescending(x => x.SkillLevel)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            ValidateTeam(teamId);

            return _players.Where(x => x.TeamId == teamId)
                           .OrderByDescending(x => x.Salary)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            ValidateTeam(teamId);

            return _players.Where(x => x.TeamId == teamId)
                           .OrderBy(x => x.BirthDate)
                           .Select(x => x.Id)
                           .FirstOrDefault();
        }

        public string GetPlayerName(long playerId)
        {
            ValidatePlayer(playerId);

            return _players.Where(x => x.Id == playerId)
                           .Select(x => x.Name)
                           .FirstOrDefault();
        }

        public decimal GetPlayerSalary(long playerId)
        {
            ValidatePlayer(playerId);

            return _players.Where(x => x.Id == playerId)
                           .Select(x => x.Salary)
                           .FirstOrDefault();
        }

        public long GetTeamCaptain(long teamId)
        {
            ValidateTeam(teamId);

            Player player = _players.Where(x => x.TeamId == teamId && x.Captain).FirstOrDefault();
            if (player == null)
                throw new CaptainNotFoundException();

            return player.Id;
        }

        public string GetTeamName(long teamId)
        {
            ValidateTeam(teamId);

            return _teams.Where(x => x.Id == teamId)
                         .Select(x => x.Name)
                         .FirstOrDefault();
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            ValidateTeam(teamId);

            return _players.Where(x => x.TeamId == teamId)
                           .OrderBy(x => x.Id)
                           .Select(x => x.Id)
                           .ToList();
        }

        public List<long> GetTeams()
        {
            return _teams.Select(x => x.Id)
                         .OrderBy(x => x)
                         .ToList();
        }

        public List<long> GetTopPlayers(int top)
        {
            return _players.OrderByDescending(x => x.SkillLevel)
                           .Select(x => x.Id)
                           .Take(top)
                           .ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            // Validando time da casa
            ValidateTeam(teamId);

            // Validando time visitante
            ValidateTeam(visitorTeamId);

            Team team = _teams.Where(x => x.Id == teamId).FirstOrDefault();
            Team visitorTeam = _teams.Where(x => x.Id == visitorTeamId).FirstOrDefault();

            return team.MainShirtColor == visitorTeam.MainShirtColor ? visitorTeam.SecondaryShirtColor : visitorTeam.MainShirtColor;
        }

        public void SetCaptain(long playerId)
        {
            ValidatePlayer(playerId);
            _players.ForEach(player => player.Captain = player.Id == playerId ? true : false);
        }

        private void ValidateTeam(long teamId)
        {
            if (!HasTeam(teamId))
                throw new TeamNotFoundException();
        }

        private void ValidatePlayer(long playerId)
        {
            if (!HasPlayer(playerId))
                throw new PlayerNotFoundException();
        }

        public bool HasTeam(long teamId)
        {
            return _teams.Any(x => x.Id == teamId);
        }

        public bool HasPlayer(long playerId)
        {
            return _players.Any(x => x.Id == playerId);
        }
    }
}
