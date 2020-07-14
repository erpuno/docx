using System;

namespace TemplateEngine.Docx
{
	[ContentItemName("Content")]
	public class Content : Container, IEquatable<Content>
	{
		public Content()
		{
		}
		public Content(IContentItem[] contentItems):base(contentItems)
		{
		}

		public bool Equals(Content other)
		{
			return base.Equals(other);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
