using DAL.Object;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    public class VoucherDAL
    {
        private Voucher _voucher;

        public VoucherDAL(string id, string description, int minimumOrderPrice, int discountAmount, int discountMaxAmount,
                          DateTime startDate, DateTime endDate, bool isActive)
        {
            _voucher = new Voucher(id,description,minimumOrderPrice,discountAmount,discountMaxAmount,startDate,endDate,isActive);
        }

        public static DataTable GetAllVouchers()
        {
            string query = "SELECT * FROM Voucher";
            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetVoucherByID()
        {
            string query = "SELECT * FROM Voucher WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _voucher.ID) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public bool InsertVoucher()
        {
            string query = "INSERT INTO Voucher (ID, [Description], [Minimum Order Price], DiscountAmount, [Discount Maximum Amount], [Start Date], [End Date]) VALUES (@ID, @Description, @MinimumOrderPrice, @DiscountAmount, @DiscountMax, @StartDate, @EndDate)";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _voucher.ID),
                new SqlParameter("@Description", _voucher.Description),
                new SqlParameter("@MinimumOrderPrice", _voucher.MinimumOrderPrice),
                new SqlParameter("@DiscountAmount", _voucher.DiscountAmount),
                new SqlParameter("@DiscountMax", _voucher.DiscountMaximumAmount),
                new SqlParameter("@StartDate", _voucher.StartDate),
                new SqlParameter("@EndDate", _voucher.EndDate)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteVoucher()
        {
            string query = "DELETE FROM Voucher WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _voucher.ID) };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateVoucher()
        {
            string query = "UPDATE Voucher SET Description = @Description, [Minimum Order Price] = @MinimumOrderPrice, [Discount Amount] = @DiscountAmount, [Discount Maximum Amount] = @DiscountMax, [Start Date] = @StartDate, [End Date] = @EndDate, [Is Active] = @IsActive WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Description", _voucher.Description ?? (object)DBNull.Value),
                new SqlParameter("@MinimumOrderPrice",_voucher.MinimumOrderPrice),
                new SqlParameter("@DiscountAmount", _voucher.DiscountAmount),
                new SqlParameter("@DiscountMax", _voucher.DiscountMaximumAmount),
                new SqlParameter("@StartDate", _voucher.StartDate),
                new SqlParameter("@EndDate", _voucher.EndDate),
                new SqlParameter("@IsActive", (_voucher.IsActive ? 1 : 0)),
                new SqlParameter("@ID", _voucher.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
