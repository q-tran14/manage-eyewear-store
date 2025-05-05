using DAL;
using DAL.Object;
using System.Data;

namespace BLL
{
    public class VoucherBLL
    {
        private VoucherDAL _voucher;

        public VoucherBLL(string id, string description, int minimumOrderPrice, int discountAmount, int discountMaxAmount,
                          DateTime startDate, DateTime endDate, bool isActive)
        {
            _voucher = new VoucherDAL(id, description, minimumOrderPrice, discountAmount, discountMaxAmount, startDate, endDate, isActive);
        }

        public static DataTable GetAllVouchers() => VoucherDAL.GetAllVouchers();

        public Voucher GetVoucherByID() 
        {
            DataTable dt = _voucher.GetVoucherByID();
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new Voucher(
                row["ID"].ToString(),
                row["Description"].ToString(),
                Convert.ToInt32(row["Minimum Order Price"]),
                Convert.ToInt32(row["Discount Amount"]),
                Convert.ToInt32(row["Discount Maximum Amount"]),
                Convert.ToDateTime(row["Start Date"]),
                Convert.ToDateTime(row["End Date"]),
                Convert.ToBoolean(row["Is Active"])
            );
        } 

        public bool AddVoucher() => _voucher.InsertVoucher();

        public bool DeleteVoucher() => _voucher.DeleteVoucher();

        public bool UpdateVoucher() => _voucher.UpdateVoucher();
    }
}
