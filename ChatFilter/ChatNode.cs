using System;
using System.Collections.Generic;

namespace TextValidator
{
	internal class ChatNode
	{
		private Dictionary<Char, ChatNode>	m_Children	= new Dictionary<Char, ChatNode>();

		public	Boolean						isLeaf		{ get; set; }
		public	ChatType					type		{ get; private set; }

		public ChatNode(ChatType type=ChatType.kRestriction)
		{
			this.type = type;
		}

		public ChatNode Add(Char key, ChatType type)
		{
			ChatNode foundNode = Find(key);
			if( null == foundNode )
			{
				foundNode = new ChatNode(type);
				m_Children.Add(key, foundNode);
			}
			return foundNode;
		}

		public ChatNode Find(Char key)
		{
			if( true == m_Children.ContainsKey(key) )
				return m_Children[key];
			return null;
		}

		public void Clear()
		{
			m_Children.Clear();
		}
	}
}