/* Copyright (C) 2004 - 2011  Versant Inc.  http://www.db4o.com */

using Db4oUnit;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Common.Soda.Util;

namespace Db4objects.Db4o.Tests.Common.Soda.Classes.Simple
{
	public class STShortTestCase : SodaBaseTestCase
	{
		internal static readonly string Descendant = "i_short";

		public short i_short;

		public STShortTestCase()
		{
		}

		private STShortTestCase(short a_short)
		{
			i_short = a_short;
		}

		public override object[] CreateData()
		{
			return new object[] { new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase
				((short)0), new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase
				((short)1), new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase
				((short)99), new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase
				((short)909) };
		}

		public virtual void TestEquals()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)0));
			// Primitive default values are ignored, so we need an 
			// additional constraint:
			q.Descend(Descendant).Constrain((short)0);
			SodaTestUtil.ExpectOne(q, _array[0]);
		}

		public virtual void TestNotEquals()
		{
			IQuery q = NewQuery();
			q.Constrain(_array[0]);
			q.Descend(Descendant).Constrain((short)0).Not();
			Expect(q, new int[] { 1, 2, 3 });
		}

		public virtual void TestGreater()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)9));
			q.Descend(Descendant).Constraints().Greater();
			Expect(q, new int[] { 2, 3 });
		}

		public virtual void TestSmaller()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)1));
			q.Descend(Descendant).Constraints().Smaller();
			SodaTestUtil.ExpectOne(q, _array[0]);
		}

		public virtual void TestContains()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)9));
			q.Descend(Descendant).Constraints().Contains();
			Expect(q, new int[] { 2, 3 });
		}

		public virtual void TestNotContains()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)0));
			q.Descend(Descendant).Constrain((short)0).Contains().Not();
			Expect(q, new int[] { 1, 2 });
		}

		public virtual void TestLike()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)90));
			q.Descend(Descendant).Constraints().Like();
			SodaTestUtil.ExpectOne(q, _array[3]);
			q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)10));
			q.Descend(Descendant).Constraints().Like();
			Expect(q, new int[] {  });
		}

		public virtual void TestNotLike()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)1));
			q.Descend(Descendant).Constraints().Like().Not();
			Expect(q, new int[] { 0, 2, 3 });
		}

		public virtual void TestIdentity()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)1));
			IObjectSet set = q.Execute();
			Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase identityConstraint
				 = (Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase)set.Next();
			identityConstraint.i_short = 9999;
			q = NewQuery();
			q.Constrain(identityConstraint).Identity();
			identityConstraint.i_short = 1;
			SodaTestUtil.ExpectOne(q, _array[1]);
		}

		public virtual void TestNotIdentity()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)1));
			IObjectSet set = q.Execute();
			Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase identityConstraint
				 = (Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase)set.Next();
			identityConstraint.i_short = 9080;
			q = NewQuery();
			q.Constrain(identityConstraint).Identity().Not();
			identityConstraint.i_short = 1;
			Expect(q, new int[] { 0, 2, 3 });
		}

		public virtual void TestConstraints()
		{
			IQuery q = NewQuery();
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)1));
			q.Constrain(new Db4objects.Db4o.Tests.Common.Soda.Classes.Simple.STShortTestCase(
				(short)0));
			IConstraints cs = q.Constraints();
			IConstraint[] csa = cs.ToArray();
			if (csa.Length != 2)
			{
				Assert.Fail("Constraints not returned");
			}
		}
	}
}
