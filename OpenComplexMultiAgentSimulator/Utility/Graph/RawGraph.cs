using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenComplexMultiAgentSimulator
{
    public class Node
    {
        public int ID { get; set; }
    }

    public class Link
    {
        public int Source { get; set; }
        public int Target { get; set; }
    }

    public class RawGraph
    {
        public bool Directed { get; set; }
        public bool Multigraph { get; set; }
        //public string Graph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }
        internal GraphEnum MyGraphEnum { get; set; }

        public List<Link> GetLinksOfSource(int source_id)
        {
            return this.Links.Where(l => l.Source == source_id).ToList();
        }
    }
}
