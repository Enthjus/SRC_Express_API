using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface RefundService
    {
        public List<RefundInfo> FindAllRefund();
        public RefundInfo FindRefund(int idRefund);
        public RefundInfo UpdateRefund(RefundInfo refundInfo);
        public List<RequestRefundInfo> FindAllRequestRefund();
        public Boolean UpdateStatusRequestRefund(int idRequestRefund);
        public Boolean SendMail(string to, string subject, string content, bool isHTML);
    }
}
