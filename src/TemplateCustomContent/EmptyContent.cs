using System;
using System.Linq;

namespace TemplateEngine.Docx
{
	[ContentItemName("Empty")]
	public class EmptyContent : HiddenContent<EmptyContent>, IEquatable<EmptyContent>
    {
        public EmptyContent()
        {
        }

        public bool Equals(EmptyContent other)
		{
			return true;
		}

		public override bool Equals(IContentItem other)
		{
			if (other is EmptyContent) return true;
			return false;
		}

		public override int GetHashCode()
		{
			return new {}.GetHashCode();
		}

	}
}
