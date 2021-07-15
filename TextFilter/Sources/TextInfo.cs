using System;

namespace TextFiltering
{
	/// <summary>
	/// 필터링 된 텍스트 결과 값 입니다.
	/// </summary>
	public struct TextInfo
	{
		/// <summary>
		/// 원본 텍스트입니다.
		/// </summary>
		public String	original { get; private set; }

		/// <summary>
		/// 필터링 된 텍스트입니다.
		/// </summary>
		public String	filtrate { get; private set; }

		/// <summary>
		/// 문제가 되었던 단어 중 마지막 단어를 추출합니다.
		/// </summary>
		public String[]	residues { get; private set; }

		/// <summary>
		/// 필터링 결과입니다.
		/// </summary>
		public Boolean isFiltered
		{
			get { return 0 < this.residues.Length; }
		}

		public TextInfo(String original, String filtrate, String[] residues)
		{
			this.original = original;
			this.filtrate = filtrate;
			this.residues = residues;
		}

		public static implicit operator bool(TextInfo textInfo)
		{
			return textInfo.isFiltered;
		}
	}
}