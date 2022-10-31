using MathNet.Numerics;
namespace SmartMobility.API.Tools
{
    public class CumulativeFunction 
    {

        public static double F(double x)
        {
            MathNet.Numerics.Distributions.Normal result = new MathNet.Numerics.Distributions.Normal();
            return result.CumulativeDistribution(x);
        }
    }
}
