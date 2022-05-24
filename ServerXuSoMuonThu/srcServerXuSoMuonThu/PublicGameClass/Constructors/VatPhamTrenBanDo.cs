namespace PublicGameClass.Constructors
{
    [System.Serializable]
    public class VatPhamTrenBanDo
    {
        public int ViTri { get; set; }
        public int ItemID { get; set; }

        public VatPhamTrenBanDo(int vitri, int id)
        {
            ViTri = vitri;
            ItemID = id;
        }

        public override string ToString()
        {
            return $"Vật phẩm {ItemID} có vị trí {ViTri}.";
        }
    }
}
