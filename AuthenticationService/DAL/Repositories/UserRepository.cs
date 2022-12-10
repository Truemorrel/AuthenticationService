using AuthenticationService.Model.DL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _userRepository = new List<User>();
        public UserRepository()
        {
            _userRepository.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Дмитрий",
                LastName = "Матрасов",
                Email = "dmatros@yandex.ru",
                Password = "543216",
                Login = "matros",
                Role = new Role()
                {
                    Id = 1,
                    Name = "Пользователь"
                }
            });
            _userRepository.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Валерия",
                LastName = "Нежданова",
                Email = "topfanky@hotmail.com",
                Password = "pleasehipchoose",
                Login = "lerok",
                Role = new Role()
                {
                    Id = 2,
                    Name = "Администратор"
                }
            });
            _userRepository.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Петр",
                LastName = "Дятлов",
                Email = "peterwood@gmail.com",
                Password = "432156",
                Login = "woodpecker",
                Role = new Role()
                {
                    Id = 1,
                    Name = "Пользователь"
                }
            });
        }
        public IEnumerable<User> GetAll()
        {

            return _userRepository;

        }
        public User GetByLogin(string login)
        {
            return _userRepository.FirstOrDefault(x => x.Login == login);
        }
    }
}
