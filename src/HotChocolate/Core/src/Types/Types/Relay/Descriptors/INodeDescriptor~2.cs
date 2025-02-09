using System;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Resolvers;

#nullable enable

namespace HotChocolate.Types.Relay.Descriptors;

/// <summary>
/// The node descriptor allows to configure a node type.
/// </summary>
public interface INodeDescriptor<TNode, out TId> : IDescriptor
{
    /// <summary>
    /// Specifies a delegate to resolve the node from its id.
    /// </summary>
    /// <param name="nodeResolver">
    /// The delegate to resolve the node from its id.
    /// </param>
    [Obsolete("Use ResolveNode.")]
    IObjectFieldDescriptor NodeResolver(
        NodeResolverDelegate<TNode, TId> nodeResolver);

    /// <summary>
    /// Specifies a delegate to resolve the node from its id.
    /// </summary>
    /// <param name="fieldResolver">
    /// The delegate to resolve the node from its id.
    /// </param>
    IObjectFieldDescriptor ResolveNode(
        FieldResolverDelegate fieldResolver);

    /// <summary>
    /// Specifies a delegate to resolve the node from its id.
    /// </summary>
    /// <param name="fieldResolver">
    /// The delegate to resolve the node from its id.
    /// </param>
    IObjectFieldDescriptor ResolveNode(
        NodeResolverDelegate<TNode, TId> fieldResolver);

    /// <summary>
    /// Specifies a member expression from which the node resolver is compiled from.
    /// </summary>
    /// <param name="method">
    /// The node resolver member expression.
    /// </param>
    /// <typeparam name="TResolver">
    /// The declaring node resolver member type.
    /// </typeparam>
    IObjectFieldDescriptor ResolveNodeWith<TResolver>(
        Expression<Func<TResolver, object?>> method);

    /// <summary>
    /// Specifies a type from which the node resolver shall be inferred from.
    /// </summary>
    /// <typeparam name="TResolver">
    /// The type that contains the node resolver.
    /// </typeparam>
    IObjectFieldDescriptor ResolveNodeWith<TResolver>();

    /// <summary>
    /// Specifies a method from which a node resolver shall be compiled from.
    /// </summary>
    /// <param name="method">
    /// The node resolver method.
    /// </param>
    IObjectFieldDescriptor ResolveNodeWith(MethodInfo method);

    /// <summary>
    /// Specifies a type from which the node resolver shall be inferred from.
    /// </summary>
    /// <param name="type">
    /// The type that contains the node resolver.
    /// </param>
    IObjectFieldDescriptor ResolveNodeWith(Type type);
}
