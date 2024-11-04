using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractionApp
{
    public interface IServices
    {
        void CreateUser();
        bool GetCustomer(string customerID);
        void ViewShipperDetail();
        void OrderSummary();
        void PreviousOrder(string customerID);
    }
}
