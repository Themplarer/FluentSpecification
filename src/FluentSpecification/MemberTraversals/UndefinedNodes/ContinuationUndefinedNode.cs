// MIT License
// 
// Copyright (c) 2021 Alexey Politov
// https://github.com/EmptyBucket/FluentSpecification
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentSpecification.MemberTraversals.MemberNodes;

namespace FluentSpecification.MemberTraversals.UndefinedNodes
{
	internal class ContinuationUndefinedNode<TRoot, TPrev, T> : IUndefinedNode<TRoot, T>
	{
		private readonly IMemberNode<TRoot, TPrev> _prevMemberNode;
		private readonly Expression<Func<TPrev, T>> _member;

		public ContinuationUndefinedNode(IMemberNode<TRoot, TPrev> prevMemberNode, Expression<Func<TPrev, T>> member) =>
			(_prevMemberNode, _member) = (prevMemberNode, member);

		public IMemberNode<TRoot, T> ToOneMemberNode() =>
			new ContinuationOneMemberNode<TRoot, TPrev, T>(_prevMemberNode, _member);

		public IMemberNode<TRoot, TItem> ToManyMemberNode<TItem>() =>
			new ContinuationManyMemberNode<TRoot, TPrev, TItem>(_prevMemberNode,
				Expression.Lambda<Func<TPrev, IEnumerable<TItem>>>(_member.Body, _member.Parameters));
	}
}