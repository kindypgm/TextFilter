using System;

namespace TextValidator
{
	/// <summary>
	/// 필터링 된 텍스트 결과 값 입니다.
	/// </summary>
	public struct ChatInfo
	{
		/// <summary>
		/// 원본 텍스트입니다.
		/// </summary>
		public String original	{ get; private set; }
		/// <summary>
		/// 문제가 되었던 단어를 추출합니다.
		/// </summary>
		public String residue	{ get; set; }
		/// <summary>
		/// 필터링 된 텍스트입니다.
		/// </summary>
		public String filtrate	{ get; set; }

		public ChatInfo(String original)
		{
			this.original	= original;
			this.residue	= String.Empty;
			this.filtrate	= original;
		}

		public static implicit operator String(ChatInfo chatInfo)
		{
			return chatInfo.filtrate;
		}
	}
}