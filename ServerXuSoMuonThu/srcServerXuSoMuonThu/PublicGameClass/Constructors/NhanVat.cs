using System;

namespace PublicGameClass.Constructors
{
    [Serializable]
    public class NhanVat
    {
        public int IDtaikhoan;
        public string TenNhanVat;
        public int STT;
        public bool GioiTinh;
        public int TrangThai;
        public DateTime NgayTaoNhanVat;
        public int TrangPhuc;
        public int ThuCung;
        public int PetDangDanh;
        public int Ngoc;
        public int Vang;
        public Vector3c VitriHienTai;

        public NhanVat(int iDtaikhoan, string tenNhanVat, int sTT, bool gioiTinh, int trangThai,
            DateTime ngayTaoNhanVat, int trangPhuc, int thuCung, int ngoc, int vang)
        {
            IDtaikhoan = iDtaikhoan;
            TenNhanVat = tenNhanVat;
            STT = sTT;
            GioiTinh = gioiTinh;
            TrangThai = trangThai;
            NgayTaoNhanVat = ngayTaoNhanVat;
            TrangPhuc = trangPhuc;
            ThuCung = thuCung;
            Ngoc = ngoc;
            Vang = vang;
            PetDangDanh = 0;
        }

        /// <summary>
        /// xuất ra nhân vật lỗi
        ///  -1 chưa có nhân vật, -2 lỗi truy vấn ở db exception
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static NhanVat ErrorNhanVat(int error)
        {
            return new NhanVat(error, "", error, false, error, DateTime.UtcNow, error, -1, error, error);
        }

        /// <summary>
        /// Tạo nhân vật mới với ID, Tên và Giới Tính
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenNhanVat"></param>
        /// <param name="gioitinh"></param>
        /// <returns></returns>
        public static NhanVat NhanVatMoi(int id, string tenNhanVat, bool gioitinh)
        {
            return new NhanVat(id, tenNhanVat, 1, gioitinh, 0, DateTime.UtcNow, 0, 0, 0, 0);
        }
    }
}
