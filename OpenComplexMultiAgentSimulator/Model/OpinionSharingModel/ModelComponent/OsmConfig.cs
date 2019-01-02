using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    class OsmConfig
    {
        //graph
        //graph enum
        public int GraphSeed = -1;
        public GraphEnum MyGraphEnum = GraphEnum.Void;
        public int GraphSize = -1;

        //ws
        public int NearestNeighbors = -1;
        public double RewireProbability = -1;

        //ba
        public int AttachEdges = -1;

        //er
        public double EdgeCreateProbability = -1;

        //pc
        public int RandomEdges;
        public double AddTriangleProbability = -1;

        //grid
        public int Width = -1;
        public int Height = -1;

        //layout
        public LayoutEnum MyLayoutEnum = LayoutEnum.Circular;


        //osm
        //subject
        public string SubjectName = null;
        public int SubjectOpinionDim = -1;

        //environment
        public int CorrectDim = -1;
        public double SensorRate = -1;

        //agent
        public int AgentGeneSeed = -1;
        public double OpinionFormThreshold = -1;
        public Vector<double> InitialOpinionVector = null;
        public int SensorSize = -1;
        public double SensorSizeRate = -1;
        public InitBeliefMode MyInitBeliefMode = InitBeliefMode.NoRandom;

        //optimization
        public OsmOptimization MyOsmOptimization = OsmOptimization.Null;

        //aat
        public double TargetH = -1;
        public int OpinionIntroInterval = -1;
        public double OpinionIntroRate = -1;
        public double SensorCommonWeight = -1;
        public double AgentsCommonWeight = -1;

        //model
        public int UpdateModelSeed = -1;
        public int Rounds = -1;
        public int Steps = -1;

        //log
        public string LogFolderName = null;
    }
}
