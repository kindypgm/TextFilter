using System;

namespace TextFiltering
{
	/// <summary>
	/// ���͸� �� �ؽ�Ʈ ��� �� �Դϴ�.
	/// </summary>
	public struct TextInfo
	{
		/// <summary>
		/// ���� �ؽ�Ʈ�Դϴ�.
		/// </summary>
		public String	original { get; private set; }

		/// <summary>
		/// ���͸� �� �ؽ�Ʈ�Դϴ�.
		/// </summary>
		public String	filtrate { get; private set; }

		/// <summary>
		/// ������ �Ǿ��� �ܾ� �� ������ �ܾ �����մϴ�.
		/// </summary>
		public String[]	residues { get; private set; }

		/// <summary>
		/// ���͸� ����Դϴ�.
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