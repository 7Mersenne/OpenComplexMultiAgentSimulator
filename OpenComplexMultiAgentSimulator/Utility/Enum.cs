using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    enum ModelEnum
    {
        PryymakOpinionSharingModel,
        //PryymakOpinionSharingModelWithAAT,

    }


    enum GraphEnum
    {
        WattsStrogatz,
        NewmanWattsStrogatz,
        ConnectedWattsStrogatz,
        BarabasiAlbert,
        FastGnp,
        GnpRandom,
        DenseGnm,
        Gnm,
        ErdosRenyi,
        Binomial,
        RandomRegular,
        PowerLawCluster,
        RandomKernel,
        RandomLobster,
        Grid2D,
        Hexagonal,
        Triangular,
        Custom,
        Void
    }


    enum LayoutEnum
    {
        Circular,
        FruchtermanReingold,
        KamadaKawai,
        Random,
        Shell,
        Spectral,
        Spring,
        Square,
        Null
    }


    enum InitBeliefMode
    {
        NormalNarrow,
        Normal,
        NormalWide,
        NoRandom
    }

    enum OsmOptimization
    {
        AAT,
        Null
    }



    enum SeedEnum
    {
        AgentGeneSeed,
        UpdateModelSeed,
    }


    enum CalcWeightMode
    {
        FavorMyOpinion,
        Equality
    }


    enum SampleAgentSetMode
    {
        RandomSetRate,
        RemainSet
    }
}
