using ExitGames.Logging;
using Newtonsoft.Json;
using Photon.SocketServer;
using PublicGameClass.Constructors;
using PublicGameClass.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.Rooms
{
    public class Room
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public BanDoCode code;
        public int KhuVuc; // list client trong phòng
        public List<User> users; // list client trong phòng
        public List<NhanVat> NhanVats = new List<NhanVat>();
        public int TongViTri, ViTriCanDung;
        public List<int> ViTriChuaDungPet, Items, Pets;
        public Queue<int> HangChoChuyenQuaChuaDungPet;
        public List<Pet> ViTriDangDungPet; // chứa pet hoang dã nào đang đứng ở vị trí nào trên bản đồ
        public Vector3c VitriMacDinh0, VitriMacDinh1, VitriMacDinh2, VitriMacDinh10; // sau sửa thành Dict
        public int maxPlayer = 24;

        public Room()
        {
            users = new List<User>();
        }

        public void SendAllPlayer(byte code, Dictionary<byte, object> data, bool useUDP = false)
        {
            for (int i = 0; i < users.Count; i++)
            {
                users[i].SendEvent(new EventData(code, data), new SendParameters { Unreliable = useUDP });
            }
        }

        public void SendAllPlayerOther(byte code, Dictionary<byte, object> data, User otherUser, bool useUDP = false)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i] != otherUser)
                    users[i].SendEvent(new EventData(code, data), new SendParameters { Unreliable = useUDP });
            }
        }

        public void JoinRoom(User user, int ViTriMacDinh)
        {
            if (users.Count >= maxPlayer)
            {
                // ném throw thì sẽ out function
                throw new Exception("RoomHasFull");
            }
            if (user.RoomHienTai != null) user.RoomHienTai.LeaveRoom(user);
            switch (ViTriMacDinh)
            {
                case 0:
                    {
                        user.NhanVatHienTai.VitriHienTai = VitriMacDinh0;
                        break;
                    }
                case 1:
                    {
                        user.NhanVatHienTai.VitriHienTai = VitriMacDinh1;
                        break;
                    }
                case 2:
                    {
                        user.NhanVatHienTai.VitriHienTai = VitriMacDinh2;
                        break;
                    }
                case 10:
                    {
                        user.NhanVatHienTai.VitriHienTai = VitriMacDinh10;
                        break;
                    }
            }
            users.Add(user);
            user.RoomHienTai = this;
            NhanVats.Add(user.NhanVatHienTai);
        }

        public void LeaveRoom(User user)
        {
            users.Remove(user);
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data[1] = (int)RoomCode.LeaveRoom;
            data[2] = user.NhanVatHienTai.IDtaikhoan;
            SendAllPlayer((byte)RequestCode.Room, data);
            NhanVats.RemoveAll(x => x.IDtaikhoan == user.NhanVatHienTai.IDtaikhoan);
            Log.Debug($"Nhân Vật {user.NhanVatHienTai.TenNhanVat} rời, phòng còn {NhanVats.Count} NV");
            user.RoomHienTai = null;
        }

        public void SinhPet(int thoigiancho = 80)
        {
            Thread.Sleep(thoigiancho);
            Random r = new Random();
            int typePet = PetNgauNhien();
            int vitri = r.Next(ViTriChuaDungPet.Count);
            var newPet = new Pet();
            newPet.petPos = ViTriChuaDungPet[vitri];
            newPet.petType = typePet;
            newPet.level = r.Next(5);
            newPet.attributes = petAttributes.AttributesPetHoangDa(newPet.level);
            ViTriDangDungPet.Add(newPet);
            ViTriChuaDungPet.RemoveAt(vitri);

            Dictionary<byte, object> returnData = new Dictionary<byte, object>();
            returnData[1] = GamePlayCode.SinhPet;
            returnData[2] = JsonConvert.SerializeObject(newPet);
            SendAllPlayer((byte)RequestCode.GamePlay, returnData);

            if (HangChoChuyenQuaChuaDungPet.Count > 0)
            {
                ViTriChuaDungPet.Add(HangChoChuyenQuaChuaDungPet.Dequeue());
            }
        }

        public int ItemNgauNhien()
        {
            Thread.Sleep(70);
            int itemID = -1;
            Random r = new Random();
            int temp = r.Next(1200);

            if (temp < 1200)
                itemID = 1;
            if (temp < 600)
                itemID = 2;
            if (temp < 320)
                itemID = 3;
            if (temp < 180)
                itemID = 4;
            if (temp < 90)
                itemID = 5;
            if (temp < 30)
                itemID = 6;
            if (temp < 4)
                itemID = 7;

            return itemID;
        }

        public int PetNgauNhien()
        {
            Thread.Sleep(70);
            Random r = new Random();
            int index = r.Next(Pets.Count);
            int Pet = Pets[index];
            return Pet;
        }

        public bool DuoiPetHoangDa(int vitri, User user)
        {
            lock (ViTriDangDungPet)
            {
                if (ViTriDangDungPet.Find(x => x.petPos == vitri) != null)
                {
                    Dictionary<byte, object> returnData = new Dictionary<byte, object>();
                    returnData[1] = GamePlayCode.DuoiPetHoangDa;
                    returnData[2] = vitri;
                    returnData[3] = user.NhanVatHienTai.IDtaikhoan;
                    SendAllPlayer((byte)RequestCode.GamePlay, returnData);
                    return true;
                }
                else return false;
            }
        }

        public void DuoiPetHoangDaTC(int vitri, User user)
        {
            ViTriDangDungPet.RemoveAll(x => x.petPos == vitri);
            HangChoChuyenQuaChuaDungPet.Enqueue(vitri); // thêm vào hàng chờ, đợi vài s rồi thêm pet mới
            var idItemNhanDuoc = ItemNgauNhien();
            Dictionary<byte, object> returnData = new Dictionary<byte, object>();
            returnData[1] = GamePlayCode.DuoiPetHoangDaThanhCong;
            returnData[2] = vitri;
            returnData[3] = user.NhanVatHienTai.IDtaikhoan;
            returnData[4] = idItemNhanDuoc; // đuổi xong sẽ có item quà
            SendAllPlayer((byte)RequestCode.GamePlay, returnData);


            if (user.DictItem.ContainsKey(idItemNhanDuoc))
            {
                World.Instance.addQuery($"UPDATE inventoryitem SET " +
                    $"SoLuong='{++user.DictItem[idItemNhanDuoc].SoLuong}' WHERE " +
                    $"IDtaikhoan = '{user.NhanVatHienTai.IDtaikhoan}' and itemID = '{idItemNhanDuoc}'");
            }
            else
            {
                World.Instance.addQuery($"INSERT INTO inventoryitem " +
                    $"VALUES ('{user.NhanVatHienTai.IDtaikhoan}','{idItemNhanDuoc}','1')");
                user.DictItem[idItemNhanDuoc] = new InventoryItem(idItemNhanDuoc, 1);
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                PetHoangDaMoi(state);
            }), cts.Token);
        }

        void PetHoangDaMoi(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            if (token.IsCancellationRequested)
            {
                return;
            }
            SinhPet(10000);
        }
    }
}
