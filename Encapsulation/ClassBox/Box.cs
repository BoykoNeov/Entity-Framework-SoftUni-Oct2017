namespace ClassBox
{
    using System;

    public class Box
    {
        private decimal width;
        private decimal height;
        private decimal length;

        public Box(decimal inputLength, decimal inputWidth, decimal inputHeight)
        {
            this.Length = inputLength;
            this.Width = inputWidth;
            this.Height = inputHeight;
        }

        public Box()
        { }

        public decimal Length
        {
            get
            {
                return this.length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }
                else
                {
                    this.length = value;
                }
            }
        }

        public decimal Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }
                else
                {
                    this.height = value;
                }
            }
        }


        public decimal Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }
                else
                {
                    this.width = value;
                }
            }
        }

        //        Volume = lwh
        //Lateral Surface Area = 2lh + 2wh
        //Surface Area = 2lw + 2lh + 2wh


        public decimal CalculateSurfaceArea()
        {
            decimal result = (2 * this.length * this.width) + (2 * this.length * this.height) + (2 * this.width * this.height);
            return result;
        }

        public decimal CalculateLateralSurfaceArea()
        {
            decimal result = (2 * this.length * this.height) + (2 * this.width * this.height);
            return result;
        }

        public decimal CalculateVolume()
        {
            decimal result = (this.length * this.width * this.height);
            return result;
        }
    }
}