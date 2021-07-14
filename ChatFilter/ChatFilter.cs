using System;

namespace TextValidator
{
	/// <summary>
	/// 단어 타입.
	/// </summary>
	public enum ChatType
	{
		/// <summary>
		/// 필터링 되는 단어입니다. (기본 값)
		/// </summary>
		kRestriction,
		/// <summary>
		/// 필터링에 해당된 단어도 예외 문장을 만나면 무시됩니다.
		/// </summary>
		kException
	}

	/// <summary>
	/// 단어 중간 중간에 섞여있는 노이즈를 무시합니다.
	/// </summary>
	public delegate Boolean NoiseValidation(Char word);

	/// <summary>
	/// 채팅 필터 클래스.
	/// </summary>
	public class ChatFilter
	{
		/// <summary>
		/// 치환할 단어.
		/// </summary>
		public static Char				Replacement		= '*';
		/// <summary>
		/// 무시할 단어들을 델리게이트 함수로 파악합니다.
		/// </summary>
		public static NoiseValidation	NoiseValidation	= null;
		
		private ChatNode				m_Root			= new ChatNode();

		/// <summary>
		/// 필터링 될 단어를 추가합니다.
		/// </summary>
		public void Add(String word)
		{
			Add(word, ChatType.kRestriction);
		}

		/// <summary>
		/// 무효화 할 문장 추가합니다.
		/// </summary>
		public void Invalidate(String word)
		{
			Add(word, ChatType.kException);
		}

		/// <summary>
		/// 필터를 초기화 합니다.
		/// </summary>
		public void Clear()
		{
			m_Root.Clear();
		}

		/// <summary>
		/// 필터링을 시도합니다.
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