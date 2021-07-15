using System;

namespace TextFiltering
{
	internal struct MatchInfo
	{
		public Int32				matchCount	{ get; private set; }
		public TextNode.NodeType	nodeType	{ get; private set; }

		public MatchInfo(Int32 matchCount, TextNode.NodeType nodeType)
		{
			this.matchCount	= matchCount;
			this.nodeType	= nodeType;
		}
	}
}