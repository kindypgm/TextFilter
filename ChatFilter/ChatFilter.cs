using System;

namespace TextValidator
{
	/// <summary>
	/// �ܾ� Ÿ��.
	/// </summary>
	public enum ChatType
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

	/// <summary>
	/// �ܾ� �߰� �߰��� �����ִ� ����� �����մϴ�.
	/// </summary>
	public delegate Boolean NoiseValidation(Char word);

	/// <summary>
	/// ä�� ���� Ŭ����.
	/// </summary>
	public class ChatFilter
	{
		/// <summary>
		/// ġȯ�� �ܾ�.
		/// </summary>
		public static Char				Replacement		= '*';
		/// <summary>
		/// ������ �ܾ���� ��������Ʈ �Լ��� �ľ��մϴ�.
		/// </summary>
		public static NoiseValidation	NoiseValidation	= null;
		
		private ChatNode				m_Root			= new ChatNode();

		/// <summary>
		/// ���͸� �� �ܾ �߰��մϴ�.
		/// </summary>
		public void Add(String word)
		{
			Add(word, ChatType.kRestriction);
		}

		/// <summary>
		/// ��ȿȭ �� ���� �߰��մϴ�.
		/// </summary>
		public void Invalidate(String word)
		{
			Add(word, ChatType.kException);
		}

		/// <summary>
		/// ���͸� �ʱ�ȭ �մϴ�.
		/// </summary>
		public void Clear()
		{
			m_Root.Clear();
		}

		/// <summary>
		/// ���͸��� �õ��մϴ�.
		/// </summary>
		public ChatInfo Filter(String chat)
		{
			ChatInfo chatInfo = new ChatInfo(ConvertCase(chat));
			
			for(Int32 start=0, len=chatInfo.original.Length; start<len;)
			{
				MatchInfo matchInfo = Match(chatInfo.filtrate.Substring(start, len-start), m_Root);
				if( 0 < matchInfo.matchCount )
				{
					if( ChatType.kRestriction == matchInfo.chatType )
						chatInfo.filtrate = Replace(chatInfo.filtrate.ToCharArray(), start, matchInfo.matchCount);
					start += matchInfo.matchCount;
				}
				else
					start++;
			}
			return chatInfo;
		}

		private	void Add(String word, ChatType type)
		{
			String		convert	= ConvertCase(word);
			ChatNode	current	= m_Root;

			for(Int32 i=0, len=convert.Length; i<len; ++i)
			{
				if( null != current )
					current = current.Add(convert[i], type);
			}
			if( null != current )
				current.isLeaf = true;
		}

		private static String Replace(Char[] chat, Int32 start, Int32 count)
		{
			for(Int32 i=start; i<start+count; ++i)
			{
				chat[i] = ChatFilter.Replacement;
			}
			return new String(chat);
		}

		private static MatchInfo Match(String text, ChatNode root)
		{
			ChatNode	current		= root;
			MatchInfo	matchInfo	= new MatchInfo();

			for(Int32 i=0, len=text.Length; i<len; ++i)
			{
				current = current.Find(text[i]);
				if( null == current )
				{
					if( null != ChatFilter.NoiseValidation )
					{
						if( true == ChatFilter.NoiseValidation(text[i]) )
							continue;
					}
					return matchInfo;
				}

				if( true == current.isLeaf )
				{
					matchInfo.matchCount	= i+1;
					matchInfo.chatType		= current.type;
				}
			}
			return matchInfo;
		}

		private static String ConvertCase(String casedChat)
		{
			return casedChat.ToLower();
		}
	}
}