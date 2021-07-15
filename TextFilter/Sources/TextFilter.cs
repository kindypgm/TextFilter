using System;

namespace TextFiltering
{
	/// <summary>
	/// 단어 중간 중간에 섞여있는 노이즈를 무시합니다.
	/// </summary>
	public delegate Boolean NoiseFilterHandler(Char c);

	/// <summary>
	/// 텍스트 필터 클래스.
	/// </summary>
	public class TextFilter
	{
		private TextNode			m_Root			= new TextNode();
		public Char					replacement		{ get; set; }
		public NoiseFilterHandler	noiseFilter		{ get; set; }

		public TextFilter(Char replacement='*', NoiseFilterHandler noiseFilter=null)
		{
			this.replacement = replacement;
			this.noiseFilter = noiseFilter;
		}

		/// <summary>
		/// 필터링 될 단어를 추가합니다.
		/// </summary>
		public void Add(String word)
		{
			Add(word, TextNode.NodeType.kRestriction);
		}

		/// <summary>
		/// 무효화 할 문장 추가합니다.
		/// </summary>
		public void Invalidate(String word)
		{
			Add(word, TextNode.NodeType.kException);
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
		public TextInfo Filter(String text)
		{
			String		original = text;
			String		filtrate = text;
			StringList	residues = new StringList();

			for(Int32 start=0, len=text.Length; start<len;)
			{
				MatchInfo matchInfo = Match(ConvertCase(filtrate.Substring(start, len-start)), m_Root);
				if( 0 < matchInfo.matchCount )
				{
					if( TextNode.NodeType.kRestriction == matchInfo.nodeType )
					{
						residues.Add(filtrate.Substring(start, matchInfo.matchCount));
						filtrate = Replace(filtrate, start, matchInfo.matchCount);
					}
					start += matchInfo.matchCount;
				}
				else
					start++;
			}
			return new TextInfo(original, filtrate, residues);
		}

		/// <summary>
		/// 필터링 결과입니다.
		/// </summary>
		public Boolean IsFiltered(String text)
		{
			return Filter(text);
		}

		private	void Add(String word, TextNode.NodeType type)
		{
			String		convert	= ConvertCase(word);
			TextNode	current	= m_Root;

			for(Int32 i=0, len=convert.Length; i<len; ++i)
			{
				if( null != current )
					current = current.Add(convert[i], type);
			}
			if( null != current )
				current.isLeaf = true;
		}

		private String Replace(String text, Int32 start, Int32 count)
		{
			Char[] array = text.ToCharArray();
			for(Int32 i=start; i<start+count; ++i)
			{
				array[i] = this.replacement;
			}
			return new String(array);
		}

		private MatchInfo Match(String text, TextNode root)
		{
			TextNode			current		= root;
			Int32				matchCount	= 0;
			TextNode.NodeType	nodeType	= TextNode.NodeType.kRestriction;

			for(Int32 i=0, len=text.Length; i<len; ++i)
			{
				if( true == current.TryGetValue(text[i], out current, current) )
				{
					if( true == current.isLeaf )
					{
						matchCount	= i+1;
						nodeType	= current.type;
					}
				}
				else
				{
					if( null != this.noiseFilter && true == this.noiseFilter(text[i]) )
						continue;
					else
						break;
				}
			}
			return new MatchInfo(matchCount, nodeType);
		}

		private static String ConvertCase(String casedText)
		{
			return casedText.ToLower();
		}
	}
}