using System;
using System.Collections.Generic;

namespace PublicGameClass.Constructors
{
    [Serializable]
    public class DonGiaoDich
    {
        public int Id { get; set; }
        public int IDnhanvat1 { get; set; } // nguwofi choiw
        public int IDnhanvat2 { get; set; } // banj bef
        public Dictionary<int, InventoryItem> DanhSachGiaoDich1 { get; set; }
        public Dictionary<int, InventoryItem> DanhSachGiaoDich2 { get; set; }
        public int TrangThai1 { get; set; }
        public int TrangThai2 { get; set; }

        public DonGiaoDich(int id, int iDnhanvat1, int iDnhanvat2, Dictionary<int, InventoryItem> danhSachGiaoDich1, Dictionary<int, InventoryItem> danhSachGiaoDich2, int trangThai1, int trangThai2)
        {
            Id=id;
            IDnhanvat1=iDnhanvat1;
            IDnhanvat2=iDnhanvat2;
            DanhSachGiaoDich1=danhSachGiaoDich1;
            DanhSachGiaoDich2=danhSachGiaoDich2;
            TrangThai1=trangThai1;
            TrangThai2=trangThai2;
        }
    }
}
