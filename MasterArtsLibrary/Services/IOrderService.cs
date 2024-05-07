using MasterArtsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterArtsLibrary.Services
{
    public interface IOrderService
    {
        Task CreateOrderInApi(Order order);
        Task<string> GetAccessToken();

    }
}
