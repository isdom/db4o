package com.db4o.db4ounit.common.handlers;

import com.db4o.ext.*;

import db4ounit.*;

public class PlainObjectUpdateTestCase extends HandlerUpdateTestCaseBase {

	public static final class Item {

		@Override
		public int hashCode() {
			final int prime = 31;
			int result = 1;
			result = prime * result
					+ ((_typed == null) ? 0 : _typed.hashCode());
			result = prime * result
					+ ((_untyped == null) ? 0 : _untyped.hashCode());
			return result;
		}

		@Override
		public boolean equals(Object obj) {
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (getClass() != obj.getClass())
				return false;
			Item other = (Item) obj;
			return Check.objectsAreEqual(_typed, other._typed)
				&& Check.objectsAreEqual(_untyped, other._untyped);
		}

		public Object _typed;
		public Object _untyped;

		public Item(Object object) {
			_typed = object;
			_untyped = object;
		}
	}
	
	@Override
	protected boolean isApplicableForDb4oVersion() {
		return db4oMajorVersion() >= 7 && db4oMinorVersion() >= 2;
	}

	@Override
	protected void assertArrays(ExtObjectContainer objectContainer, Object obj) {
		final Object[] array = (Object[])obj;
		Assert.areEqual(2, array.length);
		Assert.areSame(array[0], array[1]);
	}

	@Override
	protected void assertValues(ExtObjectContainer objectContainer, Object[] values) {
		Assert.areEqual(1, values.length);
		final Item item = (Item)values[0];
		Assert.isNotNull(item);
		Assert.isNotNull(item._typed);
		Assert.areSame(item._typed, item._untyped);
	}

	@Override
	protected Object createArrays() {
		final Object object = new Object();
		return new Object[] { object, object };
	}

	@Override
	protected Object[] createValues() {
		return new Object[] {
			new Item(new Object()),
		};
	}

	@Override
	protected String typeName() {
		return Object.class.getName();
	}

}
