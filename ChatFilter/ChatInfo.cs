using System;

namespace TextValidator
{
	/// <summary>
	/// ���͸� �� �ؽ�Ʈ ��� �� �Դϴ�.
	/// </summary>
	public struct ChatInfo
	{
		/// <summary>
		/// ���� �ؽ�Ʈ�Դϴ�.
		/// </summary>
		public String original	{ get; private set; }
		/// <summary>
		/// ������ �Ǿ��� �ܾ �����մϴ�.
		/// </summary>
		public String residue	{ get; set; }
		/// <summary>
		/// ���͸� �� �ؽ�Ʈ�Դϴ�.
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