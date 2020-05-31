
using SoccerTeamsManager.Exceptions;
using SoccerTeamsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Codenation.Challenge
{
    public class SoccerTeamsManagerTest
    {

        private List<Team> _mockTeam = new List<Team>
        {
            new Team { Id = 1, Name = "Team 1", Created = DateTime.Parse("2020-01-01"), MainShirtColor = "cor 1", SecondaryShirtColor = "cor 2" },
            new Team { Id = 2, Name = "Team 2", Created = DateTime.Parse("2020-02-01"), MainShirtColor = "cor 2", SecondaryShirtColor = "cor 3"},
            new Team { Id = 3, Name = "Team 3", Created = DateTime.Parse("2020-03-01"), MainShirtColor = "cor 1", SecondaryShirtColor = "cor 2"},
            new Team { Id = 4, Name = "Team 4", Created = DateTime.Parse("2020-04-01"), MainShirtColor = "cor 4", SecondaryShirtColor = "cor 5" },
            new Team { Id = 5, Name = "Team 5", Created = DateTime.Parse("2020-05-01"), MainShirtColor = "cor 6", SecondaryShirtColor = "cor 4" },
            new Team { Id = 6, Name = "Team 6", Created = DateTime.Parse("2020-06-01"), MainShirtColor = "cor 4", SecondaryShirtColor = "cor 1" }
        };

        private List<Player> _mockPlayer = new List<Player>
        {
            new Player { Id = 1, TeamId = 1, Name = "Player 1", BirthDate = DateTime.Parse("1990-06-01"), Salary = 2000, SkillLevel = 1},
            new Player { Id = 2, TeamId = 1, Name = "Player 2", BirthDate = DateTime.Parse("1991-06-01"), Salary = 1500, SkillLevel = 2},
            new Player { Id = 3, TeamId = 2, Name = "Player 3", BirthDate = DateTime.Parse("1992-06-01"), Salary = 1200, SkillLevel = 3},
            new Player { Id = 4, TeamId = 2, Name = "Player 4", BirthDate = DateTime.Parse("1993-06-01"), Salary = 1000, SkillLevel = 4},
            new Player { Id = 5, TeamId = 3, Name = "Player 5", BirthDate = DateTime.Parse("1994-06-01"), Salary = 800, SkillLevel = 5},
            new Player { Id = 6, TeamId = 4, Name = "Player 6", BirthDate = DateTime.Parse("1995-06-01"), Salary = 600, SkillLevel = 6},
            new Player { Id = 7, TeamId = 5, Name = "Player 7", BirthDate = DateTime.Parse("1996-06-01"), Salary = 1490, SkillLevel = 7},
            new Player { Id = 8, TeamId = 6, Name = "Player 8", BirthDate = DateTime.Parse("1997-06-01"), Salary = 1200, SkillLevel = 8},
            new Player { Id = 9, TeamId = 6, Name = "Player 9", BirthDate = DateTime.Parse("1998-06-01"), Salary = 1300, SkillLevel = 9},
            new Player { Id = 10, TeamId = 3, Name = "Player 10", BirthDate = DateTime.Parse("1999-06-01"), Salary = 1000, SkillLevel = 10}

        };

        [Fact]
        public void Should_Be_Unique_Ids_For_Teams()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            Assert.Throws<UniqueIdentifierException>(() =>
                manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2"));
        }

        [Fact]
        public void Should_Be_Valid_Player_When_Set_Captain()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            manager.AddPlayer(1, 1, "Jogador 1", DateTime.Today, 0, 0);
            manager.SetCaptain(1);
            Assert.Equal(1, manager.GetTeamCaptain(1));
            Assert.Throws<PlayerNotFoundException>(() =>
                manager.SetCaptain(2));
        }

        [Fact]
        public void Should_Ensure_Sort_Order_When_Get_Team_Players()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");

            var playersIds = new List<long>() { 15, 2, 33, 4, 13 };
            for (int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, 0, 0);

            playersIds.Sort();
            Assert.Equal(playersIds, manager.GetTeamPlayers(1));
        }

        [Theory]
        [InlineData("10,20,300,40,50", 2)]
        [InlineData("50,240,3,1,50", 1)]
        [InlineData("10,22,24,3,24", 2)]
        public void Should_Choose_Best_Team_Player(string skills, int bestPlayerId)
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");

            var skillsLevelList = skills.Split(',').Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < skillsLevelList.Count(); i++)
                manager.AddPlayer(i, 1, $"Jogador {i}", DateTime.Today, skillsLevelList[i], 0);

            Assert.Equal(bestPlayerId, manager.GetBestTeamPlayer(1));
        }

        [Theory]
        [InlineData("Azul;Vermelho", "Azul;Amarelo", "Amarelo")]
        [InlineData("Azul;Vermelho", "Amarelo;Laranja", "Amarelo")]
        [InlineData("Azul;Vermelho", "Azul;Vermelho", "Vermelho")]
        public void Should_Choose_Right_Color_When_Get_Visitor_Shirt_Color(string teamColors, string visitorColors, string visitorMatchColor)
        {
            long teamId = 1;
            long visitorTeamId = 2;
            var teamColorList = teamColors.Split(";");
            var visitorColorList = visitorColors.Split(";");

            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(teamId, $"Time {teamId}", DateTime.Now, teamColorList[0], teamColorList[1]);
            manager.AddTeam(visitorTeamId, $"Time {visitorTeamId}", DateTime.Now, visitorColorList[0], visitorColorList[1]);

            Assert.Equal(visitorMatchColor, manager.GetVisitorShirtColor(teamId, visitorTeamId));
        }

        [Fact]
        public void Top_Jogadores()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");

            var playersIds = new List<long>() { 15, 2, 33, 4, 13, 22, 23, 26, 70 };
            for (int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, i + 10, 0);

            Assert.Equal(playersIds.Take(3).Count(), manager.GetTopPlayers(3).Count());
        }

        [Fact]
        public void Should_Player_Salary()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            var playersIds = new List<long>() { 15, 2, 33 };
            for (int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, i + 10, (i + 1) + 100);

            Assert.Equal(102, manager.GetPlayerSalary(2));
        }

        [Fact]
        public void Should_Player_Highest_Salary()
        {
            var manager = new SoccerTeamsManager.SoccerTeamsManager();
            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            var playersIds = new List<long>() { 15, 2, 33 };
            for (int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, i + 10, 100 * (i + 1));

            Assert.Equal(300, manager.GetPlayerSalary(33));
        }

        [Fact]
        public void Should_Be_Older_Team_Player()
        {
            SoccerTeamsManager.SoccerTeamsManager manager = new SoccerTeamsManager.SoccerTeamsManager();
            _mockTeam.ForEach(team => manager.AddTeam(team.Id, team.Name, team.Created, team.MainShirtColor, team.SecondaryShirtColor));
            _mockPlayer.ForEach(player => manager.AddPlayer(player.Id, player.TeamId, player.Name, player.BirthDate, player.SkillLevel, player.Salary));

            long id = manager.GetOlderTeamPlayer(1);
            Assert.Equal(1, id);
        }

        [Fact]
        public void Should_List_Player_Team_Two()
        {
            SoccerTeamsManager.SoccerTeamsManager manager = new SoccerTeamsManager.SoccerTeamsManager();
            _mockTeam.ForEach(team => manager.AddTeam(team.Id, team.Name, team.Created, team.MainShirtColor, team.SecondaryShirtColor));
            _mockPlayer.ForEach(player => manager.AddPlayer(player.Id, player.TeamId, player.Name, player.BirthDate, player.SkillLevel, player.Salary));

            List<long> ids = manager.GetTeamPlayers(2);
            Assert.Equal(2, ids.Count());
        }

        [Fact]
        public void Should_Team_Name_Equal_Team6()
        {
            SoccerTeamsManager.SoccerTeamsManager manager = new SoccerTeamsManager.SoccerTeamsManager();
            _mockTeam.ForEach(team => manager.AddTeam(team.Id, team.Name, team.Created, team.MainShirtColor, team.SecondaryShirtColor));
            _mockPlayer.ForEach(player => manager.AddPlayer(player.Id, player.TeamId, player.Name, player.BirthDate, player.SkillLevel, player.Salary));

            string name = manager.GetTeamName(6);
            Assert.Equal("Team 6", name);
        }

        [Fact]
        public void Should_Player_Name_Equal_Player1()
        {
            SoccerTeamsManager.SoccerTeamsManager manager = new SoccerTeamsManager.SoccerTeamsManager();
            _mockTeam.ForEach(team => manager.AddTeam(team.Id, team.Name, team.Created, team.MainShirtColor, team.SecondaryShirtColor));
            _mockPlayer.ForEach(player => manager.AddPlayer(player.Id, player.TeamId, player.Name, player.BirthDate, player.SkillLevel, player.Salary));

            string name = manager.GetPlayerName(1);
            Assert.Equal("Player 1", name);
        }

        [Fact]
        public void Should_Sort_All_Players_By_Skill_When_Get_Top_Players()
        {
            SoccerTeamsManager.SoccerTeamsManager manager = new SoccerTeamsManager.SoccerTeamsManager();
            _mockTeam.ForEach(team => manager.AddTeam(team.Id, team.Name, team.Created, team.MainShirtColor, team.SecondaryShirtColor));
            //_mockPlayer.ForEach(player => manager.AddPlayer(player.Id, player.TeamId, player.Name, player.BirthDate, player.SkillLevel, player.Salary));

            var playersIds = new List<long>() { 7, 33, 2, 70, 10, 240, 73, 1, 50, 17, 220, 14, 5 };
            for (int i = 0; i < playersIds.Count(); i++)
                manager.AddPlayer(playersIds[i], 1, $"Jogador {i}", DateTime.Today, (i + 1) + 10, 0);

            Assert.Equal(playersIds.Take(10).Count(), manager.GetTopPlayers(10).Count());

        }
    }
}
