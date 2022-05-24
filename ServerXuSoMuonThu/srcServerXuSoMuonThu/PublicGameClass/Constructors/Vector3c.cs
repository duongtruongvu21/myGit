namespace PublicGameClass.Constructors
{
    [System.Serializable]
    public class Vector3c
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Vector3c(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return $"x = {x}, y = {y}, z = {z};";
        }
    }
}
