namespace BookingRoomHotel.Models.ModelsInterface
{
    public interface IEmailService
    {
        public void SendMail(string TieuDe, string recip, string s);
        public void SendRegisterMail(string recip, string name, string id, string MatKhau);
        public void SendChangePasswordMail(string recip, string name, string MatKhau);
        public void SendForgotPasswordMail(string recip, string name, string MatKhau);
        public void SendConfirmQ(string recip, string name, string TieuDe);
        public void SendPhanHoiQ(string recip, string name, string TieuDe, string NoiDungQ, string PhanHoi);

    }
}
