using System;
using System.Collections.Generic;

namespace TextFiltering
{
	internal class TextNode
	{
		public enum NodeType
		{
			/// <summary>
			/// ���͸� �Ǵ� �ܾ��Դϴ�. (�⺻ ��)
			/// </summary>
			kRestriction,
			/// <summary>
			/// ���͸��� �ش�� �ܾ ���� ������ ������ ���õ˴ϴ�.
			/// </summary>
			kException
		}

		private Dictionary<Char, TextNode>	m_Children	= new Dictionary<Char, TextNode>();

		public	Boolean						isLeaf		{ get; set; }
		public	NodeType					type		{ get; private set; }

		public TextNode(NodeType type=NodeType.kRestriction)
		{
			this.type = type;
		}

		public TextNode Add(Char key, NodeType type)
		{
			if( false == m_Children.TryGetValue(key, out TextNode current) )
			{
				current = new TextNode(type);
				m_Children.Add(key, current);
			}
			return current;
		}

		public bool TryGetValue(Char key, out TextNode value, TextNode defaultValue)
		{
			return m_Children.TryGetValue(key, out value, defaultValue);
		}

		public void Clear()
		{
			m_Children.Clear();
		}
	}
}