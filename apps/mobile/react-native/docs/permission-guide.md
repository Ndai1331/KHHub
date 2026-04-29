# Permission Guide

The template ships without a permission system to keep it minimal (Home + Settings + Auth only). This guide explains how to add permission-based UI gating when you need it.

## Overview

ABP backend returns `auth.grantedPolicies` in the application configuration response (`/api/abp/application-configuration`). Each key is a policy name (e.g. `AbpIdentity.Users`), and the value is `true` when the current user has that permission.

## Adding Permission Support

### 1. Create a selector

Add to `src/store/selectors/AppSelectors.ts`:

```ts
export function createGrantedPolicySelector(condition: string) {
  return createSelector([getApp], state => {
    const grantedPolicies = state?.appConfig?.auth?.grantedPolicies;
    if (!grantedPolicies) return false;

    const hasPolicy = (policy: string) => grantedPolicies[policy.trim()] === true;

    if (condition.includes('||')) {
      return condition.split('||').some(policy => hasPolicy(policy));
    }
    if (condition.includes('&&')) {
      return condition.split('&&').every(policy => hasPolicy(policy));
    }
    return hasPolicy(condition);
  });
}
```

### 2. Create a usePermission hook

Create `src/hooks/UsePermission.ts`:

```ts
import { useSelector } from 'react-redux';
import { createGrantedPolicySelector } from '../store/selectors/AppSelectors';

export function usePermission(policyKey: string): boolean {
  const selector = createGrantedPolicySelector(policyKey);
  return useSelector(selector);
}
```

### 3. Create a permission HOC (optional)

Create `src/hocs/PermissionHOC.tsx` for hiding components conditionally:

```tsx
import { forwardRef } from 'react';
import { usePermission } from '../hooks/UsePermission';

export function withPermission<P extends object>(
  Component: React.ComponentType<P>,
  defaultPolicyKey?: string,
) {
  return forwardRef<any, P & { policyKey?: string }>((props, ref) => {
    const key = defaultPolicyKey ?? props.policyKey;
    const isGranted = key ? usePermission(key) : true;
    return isGranted ? <Component ref={ref} {...props} /> : null;
  });
}
```

### 4. Use in DrawerContent

To show a drawer item only when the user has a permission:

```tsx
const screens = {
  HomeStack: { label: '::Menu:Home', iconName: 'home' },
  UsersStack: {
    label: 'AbpIdentity::Users',
    iconName: 'account-supervisor',
    requiredPolicy: 'AbpIdentity.Users',
  },
  SettingsStack: { label: 'AbpSettingManagement::Settings', iconName: 'cog' },
};

const ListItemWithPermission = withPermission(Drawer.Item, null);

// In render:
{
  screen.requiredPolicy ? (
    <ListItemWithPermission
      policyKey={screen.requiredPolicy}
      onPress={() => navigate(name)}
      label={t(screen.label)}
      icon={screen.iconName}
    />
  ) : (
    <Drawer.Item onPress={() => navigate(name)} label={t(screen.label)} icon={screen.iconName} />
  );
}
```

### 5. Use in components

```tsx
// Inline check
const canCreate = usePermission('AbpIdentity.Users.Create');
{
  canCreate && <Button onPress={handleCreate}>Create</Button>;
}

// Or wrap with HOC
const AddButton = withPermission(Button, 'AbpIdentity.Users.Create');
```

## Policy syntax

- Single policy: `'AbpIdentity.Users'`
- OR: `'AbpIdentity.Users||AbpIdentity.Roles'`
- AND: `'AbpIdentity.Users&&AbpIdentity.Users.Create'`

## Backend

Permissions are defined in your ABP backend modules. The `getApplicationConfiguration` API returns the current user's granted policies. No backend changes are needed if you add new permission checks in the app—they just gate UI; the API still enforces authorization.
